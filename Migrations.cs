﻿using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;

namespace Etch.OrchardCore.CivicCookieControl
{
    public class Migrations : DataMigration
    {
        #region Dependencies

        private readonly IRecipeMigrator _recipeMigrator;

        #endregion

        #region Constructor

        public Migrations(IRecipeMigrator recipeMigrator)
        {
            _recipeMigrator = recipeMigrator;
        }

        #endregion

        #region Migrations

        public async Task<int> CreateAsync()
        {
            await _recipeMigrator.ExecuteAsync("create.recipe.json", this);

            return 1;
        }

        #endregion
    }
}
