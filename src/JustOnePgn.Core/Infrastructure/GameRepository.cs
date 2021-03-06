﻿using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Domain;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace JustOnePgn.Core.Infrastructure
{
    public class GameRepository : IGameRepository
    {
        private readonly string _connectionString;

        public GameRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool IsDuplicated(Game game)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                return db.Query<int>("SELECT GameId FROM Games WHERE Hash = @Hash;", new { game.Hash }).FirstOrDefault() != 0;
            }
        }

        public void Save(Game game)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var parameters = @"@Hash, @Event, @Year, 
                                   @White, @Black, @Result, 
                                   @WhiteElo, @BlackElo, @Eco, 
                                   @PlyCount, @Metadata, @Moves";

                db.Execute($@"INSERT INTO Games 
                            (Hash, Event, Year, White, Black, Result, WhiteElo, BlackElo, Eco, PlyCount, Metadata, Moves) 
                            VALUES ({parameters});", new
                {
                    game.Hash,
                    game.Event,
                    game.Year,
                    game.White,
                    game.Black,
                    game.Result,
                    game.WhiteElo,
                    game.BlackElo,
                    game.Eco,
                    game.PlyCount,
                    game.Metadata,
                    game.Moves
                });
            }
        }
    }
}