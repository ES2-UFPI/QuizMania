using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizMania.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.Data
{
    public class DatabaseInitializer
    {
        private readonly DatabaseContext _context;

        public DatabaseInitializer(DatabaseContext context)
        {
            _context = context;
        }

        public static async Task ContextSeederAsync(DatabaseContext context)
        {
            ContextAddElements(context);
            await context.SaveChangesAsync();
        }

        public async Task<bool> SeederAsync()
        {
            Startup.inMemorySqliteConnection.Close();
            Startup.inMemorySqliteConnection.Open();

            ContextAddElements(_context);

            try
            {
                await _context.SaveChangesAsync();
                return true; ;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void ContextAddElements(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            #region Add Mock Items

            // mock items
            var unitary = new ItemInfo()
            {
                Name = "Unitary Item",
                Cost = 5,
                Type = SlotType.Other,
                MaxQuantity = 1,
            }; context.Items.Add(unitary);

            var infinite = new ItemInfo()
            {
                Name = "Infinite Item",
                Cost = 3,
                Type = SlotType.Other,
                MaxQuantity = -1,
            }; context.Items.Add(infinite);

            var forbidden = new ItemInfo()
            {
                Name = "Forbiden Item",
                Cost = 0,
                Type = SlotType.Other,
                MaxQuantity = 0,
            }; context.Items.Add(forbidden);

            var face1 = new ItemInfo()
            {
                Name = "face1.png",
                Cost = 5,
                Type = SlotType.Face,
                MaxQuantity = 1,
            }; context.Items.Add(face1);

            var face2 = new ItemInfo()
            {
                Name = "face2.png",
                Cost = 5,
                Type = SlotType.Face,
                MaxQuantity = 1,
            }; context.Items.Add(face2);

            var face3 = new ItemInfo()
            {
                Name = "face3.png",
                Cost = 5,
                Type = SlotType.Face,
                MaxQuantity = 1,
            }; context.Items.Add(face3);

            var face4 = new ItemInfo()
            {
                Name = "face4.png",
                Cost = 5,
                Type = SlotType.Face,
                MaxQuantity = 1,
            }; context.Items.Add(face4);

            var blackMan1 = new ItemInfo()
            {
                Name = "blackMan1.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blackMan1);

            var blackMan2 = new ItemInfo()
            {
                Name = "blackMan2.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blackMan2);

            var blackMan3 = new ItemInfo()
            {
                Name = "blackMan3.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blackMan3);

            var blackMan4 = new ItemInfo()
            {
                Name = "blackMan4.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blackMan4);

            var blackMan5 = new ItemInfo()
            {
                Name = "blackMan5.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blackMan5);

            var blackMan6 = new ItemInfo()
            {
                Name = "blackMan6.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blackMan6);

            var blackMan7 = new ItemInfo()
            {
                Name = "blackMan7.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blackMan7);

            var blackMan8 = new ItemInfo()
            {
                Name = "blackMan8.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blackMan8);

            var blondMan1 = new ItemInfo()
            {
                Name = "blondMan1.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blondMan1);

            var blondMan2 = new ItemInfo()
            {
                Name = "blondMan2.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blondMan2);

            var blondMan3 = new ItemInfo()
            {
                Name = "blondMan3.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blondMan3);

            var blondMan4 = new ItemInfo()
            {
                Name = "blondMan4.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blondMan4);

            var blondMan5 = new ItemInfo()
            {
                Name = "blondMan5.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blondMan5);

            var blondMan6 = new ItemInfo()
            {
                Name = "blondMan6.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blondMan6);

            var blondMan7 = new ItemInfo()
            {
                Name = "blondMan7.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blondMan7);

            var blondMan8 = new ItemInfo()
            {
                Name = "blondMan8.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(blondMan8);

            var brown1Man1 = new ItemInfo()
            {
                Name = "brown1Man1.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown1Man1);

            var brown1Man2 = new ItemInfo()
            {
                Name = "brown1Man2.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown1Man2);

            var brown1Man3 = new ItemInfo()
            {
                Name = "brown1Man3.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown1Man3);

            var brown1Man4 = new ItemInfo()
            {
                Name = "brown1Man4.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown1Man4);

            var brown1Man5 = new ItemInfo()
            {
                Name = "brown1Man5.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown1Man5);

            var brown1Man6 = new ItemInfo()
            {
                Name = "brown1Man6.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown1Man6);

            var brown1Man7 = new ItemInfo()
            {
                Name = "brown1Man7.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown1Man7);

            var brown1Man8 = new ItemInfo()
            {
                Name = "brown1Man8.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown1Man8);

            var brown2Man1 = new ItemInfo()
            {
                Name = "brown2Man1.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Man1);

            var brown2Man2 = new ItemInfo()
            {
                Name = "brown2Man2.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Man2);

            var brown2Man3 = new ItemInfo()
            {
                Name = "brown2Man3.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Man3);

            var brown2Man4 = new ItemInfo()
            {
                Name = "brown2Man4.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Man4);

            var brown2Man5 = new ItemInfo()
            {
                Name = "brown2Man5.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Man5);

            var brown2Man6 = new ItemInfo()
            {
                Name = "brown2Man6.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Man6);

            var brown2Man7 = new ItemInfo()
            {
                Name = "brown2Man7.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Man7);

            var brown2Man8 = new ItemInfo()
            {
                Name = "brown2Man8.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Man8);

            var greyMan1 = new ItemInfo()
            {
                Name = "greyMan1.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(greyMan1);

            var greyMan2 = new ItemInfo()
            {
                Name = "greyMan2.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(greyMan2);

            var greyMan3 = new ItemInfo()
            {
                Name = "greyMan3.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(greyMan3);

            var greyMan4 = new ItemInfo()
            {
                Name = "greyMan4.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(greyMan4);

            var greyMan5 = new ItemInfo()
            {
                Name = "greyMan5.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(greyMan5);

            var greyMan6 = new ItemInfo()
            {
                Name = "greyMan6.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(greyMan6);

            var greyMan7 = new ItemInfo()
            {
                Name = "greyMan7.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(greyMan7);

            var greyMan8 = new ItemInfo()
            {
                Name = "greyMan8.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(greyMan8);

            var redMan1 = new ItemInfo()
            {
                Name = "redMan1.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(redMan1);

            var redMan2 = new ItemInfo()
            {
                Name = "redMan2.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(redMan2);

            var redMan3 = new ItemInfo()
            {
                Name = "redMan3.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(redMan3);

            var redMan4 = new ItemInfo()
            {
                Name = "redMan4.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(redMan4);

            var redMan5 = new ItemInfo()
            {
                Name = "redMan5.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(redMan5);

            var redMan6 = new ItemInfo()
            {
                Name = "redMan6.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(redMan6);

            var redMan7 = new ItemInfo()
            {
                Name = "redMan7.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(redMan7);

            var redMan8 = new ItemInfo()
            {
                Name = "redMan8.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(redMan8);

            var tanMan1 = new ItemInfo()
            {
                Name = "tanMan1.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(tanMan1);

            var tanMan2 = new ItemInfo()
            {
                Name = "tanMan2.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(tanMan2);

            var tanMan3 = new ItemInfo()
            {
                Name = "tanMan3.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(tanMan3);

            var tanMan4 = new ItemInfo()
            {
                Name = "tanMan4.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(tanMan4);

            var tanMan5 = new ItemInfo()
            {
                Name = "tanMan5.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(tanMan5);

            var tanMan6 = new ItemInfo()
            {
                Name = "tanMan6.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(tanMan6);

            var tanMan7 = new ItemInfo()
            {
                Name = "tanMan7.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(tanMan7);

            var tanMan8 = new ItemInfo()
            {
                Name = "tanMan8.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(tanMan8);

            var whiteMan1 = new ItemInfo()
            {
                Name = "whiteMan1.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(whiteMan1);

            var whiteMan2 = new ItemInfo()
            {
                Name = "whiteMan2.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(whiteMan2);

            var whiteMan3 = new ItemInfo()
            {
                Name = "whiteMan3.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(whiteMan3);

            var whiteMan4 = new ItemInfo()
            {
                Name = "whiteMan4.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(whiteMan4);

            var whiteMan5 = new ItemInfo()
            {
                Name = "whiteMan5.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(whiteMan5);

            var whiteMan6 = new ItemInfo()
            {
                Name = "whiteMan6.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(whiteMan6);

            var whiteMan7 = new ItemInfo()
            {
                Name = "whiteMan7.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(whiteMan7);

            var whiteMan8 = new ItemInfo()
            {
                Name = "whiteMan8.png",
                Cost = 5,
                Type = SlotType.Hair,
                MaxQuantity = 1,
            }; context.Items.Add(whiteMan8);

            var pantsBlue1_long = new ItemInfo()
            {
                Name = "pantsBlue1_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(pantsBlue1_long);

            var pantsBlue2_long = new ItemInfo()
            {
                Name = "pantsBlue2_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(pantsBlue2_long);

            var pantsBrown_long = new ItemInfo()
            {
                Name = "pantsBrown_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(pantsBrown_long);

            var pantsGreen_long = new ItemInfo()
            {
                Name = "pantsGreen_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(pantsGreen_long);

            var pantsGrey_long = new ItemInfo()
            {
                Name = "pantsGrey_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(pantsGrey_long);

            var pantsLightBlue_long = new ItemInfo()
            {
                Name = "pantsLightBlue_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(pantsLightBlue_long);

            var pantsNavy_long = new ItemInfo()
            {
                Name = "pantsNavy_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(pantsNavy_long);

            var pantsPine_long = new ItemInfo()
            {
                Name = "pantsPine_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(pantsPine_long);

            var pantsRed_long = new ItemInfo()
            {
                Name = "pantsRed_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(pantsRed_long);

            var pantsTan_long = new ItemInfo()
            {
                Name = "pantsTan_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(pantsTan_long);

            var pantsWhite_long = new ItemInfo()
            {
                Name = "pantsWhite_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(pantsWhite_long);

            var legYellow_long = new ItemInfo()
            {
                Name = "legYellow_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(legYellow_long);

            var blueArm_long = new ItemInfo()
            {
                Name = "blueArm_long.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(blueArm_long);

            var greenArm_long = new ItemInfo()
            {
                Name = "greenArm_long.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(greenArm_long);

            var greyArm_long = new ItemInfo()
            {
                Name = "greyArm_long.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(greyArm_long);

            var navyArm_long = new ItemInfo()
            {
                Name = "navyArm_long.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(navyArm_long);

            var pineArm_long = new ItemInfo()
            {
                Name = "pineArm_long.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(pineArm_long);

            var redArm_long = new ItemInfo()
            {
                Name = "redArm_long.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(redArm_long);

            var armWhite_long = new ItemInfo()
            {
                Name = "armWhite_long.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(armWhite_long);

            var armYellow_long = new ItemInfo()
            {
                Name = "armYellow_long.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(armYellow_long);

            var blueShirt1 = new ItemInfo()
            {
                Name = "blueShirt1.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(blueShirt1);

            var blueShirt2 = new ItemInfo()
            {
                Name = "blueShirt2.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(blueShirt2);

            var blueShirt3 = new ItemInfo()
            {
                Name = "blueShirt3.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(blueShirt3);

            var blueShirt4 = new ItemInfo()
            {
                Name = "blueShirt4.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(blueShirt4);

            var blueShirt5 = new ItemInfo()
            {
                Name = "blueShirt5.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(blueShirt5);

            var blueShirt6 = new ItemInfo()
            {
                Name = "blueShirt6.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(blueShirt6);

            var blueShirt7 = new ItemInfo()
            {
                Name = "blueShirt7.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(blueShirt7);

            var blueShirt8 = new ItemInfo()
            {
                Name = "blueShirt8.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(blueShirt8);

            var greenShirt1 = new ItemInfo()
            {
                Name = "greenShirt1.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greenShirt1);

            var greenShirt2 = new ItemInfo()
            {
                Name = "greenShirt2.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greenShirt2);

            var greenShirt3 = new ItemInfo()
            {
                Name = "greenShirt3.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greenShirt3);

            var greenShirt4 = new ItemInfo()
            {
                Name = "greenShirt4.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greenShirt4);

            var greenShirt5 = new ItemInfo()
            {
                Name = "greenShirt5.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greenShirt5);

            var greenShirt6 = new ItemInfo()
            {
                Name = "greenShirt6.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greenShirt6);

            var greenShirt7 = new ItemInfo()
            {
                Name = "greenShirt7.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greenShirt7);

            var greenShirt8 = new ItemInfo()
            {
                Name = "greenShirt8.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greenShirt8);

            var greyShirt1 = new ItemInfo()
            {
                Name = "greyShirt1.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greyShirt1);

            var greyShirt2 = new ItemInfo()
            {
                Name = "greyShirt2.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greyShirt2);

            var greyShirt3 = new ItemInfo()
            {
                Name = "greyShirt3.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greyShirt3);

            var greyShirt4 = new ItemInfo()
            {
                Name = "greyShirt4.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greyShirt4);

            var greyShirt5 = new ItemInfo()
            {
                Name = "greyShirt5.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greyShirt5);

            var greyShirt6 = new ItemInfo()
            {
                Name = "greyShirt6.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greyShirt6);

            var greyShirt7 = new ItemInfo()
            {
                Name = "greyShirt7.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greyShirt7);

            var greyShirt8 = new ItemInfo()
            {
                Name = "greyShirt8.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(greyShirt8);

            var navyShirt1 = new ItemInfo()
            {
                Name = "navyShirt1.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(navyShirt1);

            var navyShirt2 = new ItemInfo()
            {
                Name = "navyShirt2.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(navyShirt2);

            var navyShirt3 = new ItemInfo()
            {
                Name = "navyShirt3.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(navyShirt3);

            var navyShirt4 = new ItemInfo()
            {
                Name = "navyShirt4.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(navyShirt4);

            var navyShirt5 = new ItemInfo()
            {
                Name = "navyShirt5.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(navyShirt5);

            var navyShirt6 = new ItemInfo()
            {
                Name = "navyShirt6.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(navyShirt6);

            var navyShirt7 = new ItemInfo()
            {
                Name = "navyShirt7.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(navyShirt7);

            var navyShirt8 = new ItemInfo()
            {
                Name = "navyShirt8.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(navyShirt8);

            var pineShirt1 = new ItemInfo()
            {
                Name = "pineShirt1.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(pineShirt1);

            var pineShirt2 = new ItemInfo()
            {
                Name = "pineShirt2.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(pineShirt2);

            var pineShirt3 = new ItemInfo()
            {
                Name = "pineShirt3.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(pineShirt3);

            var pineShirt4 = new ItemInfo()
            {
                Name = "pineShirt4.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(pineShirt4);

            var pineShirt5 = new ItemInfo()
            {
                Name = "pineShirt5.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(pineShirt5);

            var pineShirt6 = new ItemInfo()
            {
                Name = "pineShirt6.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(pineShirt6);

            var pineShirt7 = new ItemInfo()
            {
                Name = "pineShirt7.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(pineShirt7);

            var pineShirt8 = new ItemInfo()
            {
                Name = "pineShirt8.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(pineShirt8);

            var redShirt1 = new ItemInfo()
            {
                Name = "redShirt1.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(redShirt1);

            var redShirt2 = new ItemInfo()
            {
                Name = "redShirt2.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(redShirt2);

            var redShirt3 = new ItemInfo()
            {
                Name = "redShirt3.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(redShirt3);

            var redShirt4 = new ItemInfo()
            {
                Name = "redShirt4.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(redShirt4);

            var redShirt5 = new ItemInfo()
            {
                Name = "redShirt5.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(redShirt5);

            var redShirt6 = new ItemInfo()
            {
                Name = "redShirt6.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(redShirt6);

            var redShirt7 = new ItemInfo()
            {
                Name = "redShirt7.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(redShirt7);

            var redShirt8 = new ItemInfo()
            {
                Name = "redShirt8.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(redShirt8);

            var whiteShirt1 = new ItemInfo()
            {
                Name = "whiteShirt1.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(whiteShirt1);

            var whiteShirt2 = new ItemInfo()
            {
                Name = "whiteShirt2.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(whiteShirt2);

            var whiteShirt3 = new ItemInfo()
            {
                Name = "whiteShirt3.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(whiteShirt3);

            var whiteShirt4 = new ItemInfo()
            {
                Name = "whiteShirt4.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(whiteShirt4);

            var whiteShirt5 = new ItemInfo()
            {
                Name = "whiteShirt5.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(whiteShirt5);

            var whiteShirt6 = new ItemInfo()
            {
                Name = "whiteShirt6.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(whiteShirt6);

            var whiteShirt7 = new ItemInfo()
            {
                Name = "whiteShirt7.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(whiteShirt7);

            var whiteShirt8 = new ItemInfo()
            {
                Name = "whiteShirt8.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(whiteShirt8);

            var shirtYellow1 = new ItemInfo()
            {
                Name = "shirtYellow1.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(shirtYellow1);

            var shirtYellow2 = new ItemInfo()
            {
                Name = "shirtYellow2.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(shirtYellow2);

            var shirtYellow3 = new ItemInfo()
            {
                Name = "shirtYellow3.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(shirtYellow3);

            var shirtYellow4 = new ItemInfo()
            {
                Name = "shirtYellow4.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(shirtYellow4);

            var shirtYellow5 = new ItemInfo()
            {
                Name = "shirtYellow5.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(shirtYellow5);

            var shirtYellow6 = new ItemInfo()
            {
                Name = "shirtYellow6.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(shirtYellow6);

            var shirtYellow7 = new ItemInfo()
            {
                Name = "shirtYellow7.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(shirtYellow7);

            var shirtYellow8 = new ItemInfo()
            {
                Name = "shirtYellow8.png",
                Cost = 12,
                Type = SlotType.Shirt,
                MaxQuantity = 1,
            }; context.Items.Add(shirtYellow8);

            var blackShoe1 = new ItemInfo()
            {
                Name = "blackShoe1.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(blackShoe1);

            var blackShoe2 = new ItemInfo()
            {
                Name = "blackShoe2.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(blackShoe2);

            var blackShoe3 = new ItemInfo()
            {
                Name = "blackShoe3.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(blackShoe3);

            var blackShoe4 = new ItemInfo()
            {
                Name = "blackShoe4.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(blackShoe4);

            var blackShoe5 = new ItemInfo()
            {
                Name = "blackShoe5.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(blackShoe5);

            var blueShoe1 = new ItemInfo()
            {
                Name = "blueShoe1.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(blueShoe1);

            var blueShoe2 = new ItemInfo()
            {
                Name = "blueShoe2.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(blueShoe2);

            var blueShoe3 = new ItemInfo()
            {
                Name = "blueShoe3.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(blueShoe3);

            var blueShoe4 = new ItemInfo()
            {
                Name = "blueShoe4.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(blueShoe4);

            var blueShoe5 = new ItemInfo()
            {
                Name = "blueShoe5.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(blueShoe5);

            var brownShoe1 = new ItemInfo()
            {
                Name = "brownShoe1.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(brownShoe1);

            var brownShoe2 = new ItemInfo()
            {
                Name = "brownShoe2.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(brownShoe2);

            var brownShoe3 = new ItemInfo()
            {
                Name = "brownShoe3.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(brownShoe3);

            var brownShoe4 = new ItemInfo()
            {
                Name = "brownShoe4.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(brownShoe4);

            var brownShoe5 = new ItemInfo()
            {
                Name = "brownShoe5.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(brownShoe5);

            var brown2Shoe1 = new ItemInfo()
            {
                Name = "brown2Shoe1.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Shoe1);

            var brown2Shoe2 = new ItemInfo()
            {
                Name = "brown2Shoe2.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Shoe2);

            var brown2Shoe3 = new ItemInfo()
            {
                Name = "brown2Shoe3.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Shoe3);

            var brown2Shoe4 = new ItemInfo()
            {
                Name = "brown2Shoe4.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Shoe4);

            var brown2Shoe5 = new ItemInfo()
            {
                Name = "brown2Shoe5.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(brown2Shoe5);

            var greyShoe1 = new ItemInfo()
            {
                Name = "greyShoe1.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(greyShoe1);

            var greyShoe2 = new ItemInfo()
            {
                Name = "greyShoe2.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(greyShoe2);

            var greyShoe3 = new ItemInfo()
            {
                Name = "greyShoe3.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(greyShoe3);

            var greyShoe4 = new ItemInfo()
            {
                Name = "greyShoe4.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(greyShoe4);

            var greyShoe5 = new ItemInfo()
            {
                Name = "greyShoe5.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(greyShoe5);

            var redShoe1 = new ItemInfo()
            {
                Name = "redShoe1.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(redShoe1);

            var redShoe2 = new ItemInfo()
            {
                Name = "redShoe2.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(redShoe2);

            var redShoe3 = new ItemInfo()
            {
                Name = "redShoe3.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(redShoe3);

            var redShoe4 = new ItemInfo()
            {
                Name = "redShoe4.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(redShoe4);

            var redShoe5 = new ItemInfo()
            {
                Name = "redShoe5.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(redShoe5);

            var tanShoe1 = new ItemInfo()
            {
                Name = "tanShoe1.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(tanShoe1);

            var tanShoe2 = new ItemInfo()
            {
                Name = "tanShoe2.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(tanShoe2);

            var tanShoe3 = new ItemInfo()
            {
                Name = "tanShoe3.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(tanShoe3);

            var tanShoe4 = new ItemInfo()
            {
                Name = "tanShoe4.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(tanShoe4);

            var tanShoe5 = new ItemInfo()
            {
                Name = "tanShoe5.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(tanShoe5);

            var tint1_arm = new ItemInfo()
            {
                Name = "tint1_arm.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(tint1_arm);

            var tint1_hand = new ItemInfo()
            {
                Name = "tint1_hand.png",
                Cost = 8,
                Type = SlotType.Hand,
                MaxQuantity = 1,
            }; context.Items.Add(tint1_hand);

            var tint1_head = new ItemInfo()
            {
                Name = "tint1_head.png",
                Cost = 8,
                Type = SlotType.Head,
                MaxQuantity = 1,
            }; context.Items.Add(tint1_head);

            var tint1_leg = new ItemInfo()
            {
                Name = "tint1_leg.png",
                Cost = 8,
                Type = SlotType.Leg,
                MaxQuantity = 1,
            }; context.Items.Add(tint1_leg);

            var tint1_neck = new ItemInfo()
            {
                Name = "tint1_neck.png",
                Cost = 8,
                Type = SlotType.Neck,
                MaxQuantity = 1,
            }; context.Items.Add(tint1_neck);

            var tint2_arm = new ItemInfo()
            {
                Name = "tint2_arm.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(tint2_arm);

            var tint2_hand = new ItemInfo()
            {
                Name = "tint2_hand.png",
                Cost = 8,
                Type = SlotType.Hand,
                MaxQuantity = 1,
            }; context.Items.Add(tint2_hand);

            var tint2_head = new ItemInfo()
            {
                Name = "tint2_head.png",
                Cost = 8,
                Type = SlotType.Head,
                MaxQuantity = 1,
            }; context.Items.Add(tint2_head);

            var tint2_leg = new ItemInfo()
            {
                Name = "tint2_leg.png",
                Cost = 8,
                Type = SlotType.Leg,
                MaxQuantity = 1,
            }; context.Items.Add(tint2_leg);

            var tint2_neck = new ItemInfo()
            {
                Name = "tint2_neck.png",
                Cost = 8,
                Type = SlotType.Neck,
                MaxQuantity = 1,
            }; context.Items.Add(tint2_neck);

            var tint3_arm = new ItemInfo()
            {
                Name = "tint3_arm.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(tint3_arm);

            var tint3_hand = new ItemInfo()
            {
                Name = "tint3_hand.png",
                Cost = 8,
                Type = SlotType.Hand,
                MaxQuantity = 1,
            }; context.Items.Add(tint3_hand);

            var tint3_head = new ItemInfo()
            {
                Name = "tint3_head.png",
                Cost = 8,
                Type = SlotType.Head,
                MaxQuantity = 1,
            }; context.Items.Add(tint3_head);

            var tint3_leg = new ItemInfo()
            {
                Name = "tint3_leg.png",
                Cost = 8,
                Type = SlotType.Leg,
                MaxQuantity = 1,
            }; context.Items.Add(tint3_leg);

            var tint3_neck = new ItemInfo()
            {
                Name = "tint3_neck.png",
                Cost = 8,
                Type = SlotType.Neck,
                MaxQuantity = 1,
            }; context.Items.Add(tint3_neck);

            var tint4_arm = new ItemInfo()
            {
                Name = "tint4_arm.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(tint4_arm);

            var tint4_hand = new ItemInfo()
            {
                Name = "tint4_hand.png",
                Cost = 8,
                Type = SlotType.Hand,
                MaxQuantity = 1,
            }; context.Items.Add(tint4_hand);

            var tint4_head = new ItemInfo()
            {
                Name = "tint4_head.png",
                Cost = 8,
                Type = SlotType.Head,
                MaxQuantity = 1,
            }; context.Items.Add(tint4_head);

            var tint4_leg = new ItemInfo()
            {
                Name = "tint4_leg.png",
                Cost = 8,
                Type = SlotType.Leg,
                MaxQuantity = 1,
            }; context.Items.Add(tint4_leg);

            var tint4_neck = new ItemInfo()
            {
                Name = "tint4_neck.png",
                Cost = 8,
                Type = SlotType.Neck,
                MaxQuantity = 1,
            }; context.Items.Add(tint4_neck);

            var tint5_arm = new ItemInfo()
            {
                Name = "tint5_arm.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(tint5_arm);

            var tint5_hand = new ItemInfo()
            {
                Name = "tint5_hand.png",
                Cost = 8,
                Type = SlotType.Hand,
                MaxQuantity = 1,
            }; context.Items.Add(tint5_hand);

            var tint5_head = new ItemInfo()
            {
                Name = "tint5_head.png",
                Cost = 8,
                Type = SlotType.Head,
                MaxQuantity = 1,
            }; context.Items.Add(tint5_head);

            var tint5_leg = new ItemInfo()
            {
                Name = "tint5_leg.png",
                Cost = 8,
                Type = SlotType.Leg,
                MaxQuantity = 1,
            }; context.Items.Add(tint5_leg);

            var tint5_neck = new ItemInfo()
            {
                Name = "tint5_neck.png",
                Cost = 8,
                Type = SlotType.Neck,
                MaxQuantity = 1,
            }; context.Items.Add(tint5_neck);

            var tint6_arm = new ItemInfo()
            {
                Name = "tint6_arm.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(tint6_arm);

            var tint6_hand = new ItemInfo()
            {
                Name = "tint6_hand.png",
                Cost = 8,
                Type = SlotType.Hand,
                MaxQuantity = 1,
            }; context.Items.Add(tint6_hand);

            var tint6_head = new ItemInfo()
            {
                Name = "tint6_head.png",
                Cost = 8,
                Type = SlotType.Head,
                MaxQuantity = 1,
            }; context.Items.Add(tint6_head);

            var tint6_leg = new ItemInfo()
            {
                Name = "tint6_leg.png",
                Cost = 8,
                Type = SlotType.Leg,
                MaxQuantity = 1,
            }; context.Items.Add(tint6_leg);

            var tint6_neck = new ItemInfo()
            {
                Name = "tint6_neck.png",
                Cost = 8,
                Type = SlotType.Neck,
                MaxQuantity = 1,
            }; context.Items.Add(tint6_neck);

            var tint7_arm = new ItemInfo()
            {
                Name = "tint7_arm.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(tint7_arm);

            var tint7_hand = new ItemInfo()
            {
                Name = "tint7_hand.png",
                Cost = 8,
                Type = SlotType.Hand,
                MaxQuantity = 1,
            }; context.Items.Add(tint7_hand);

            var tint7_head = new ItemInfo()
            {
                Name = "tint7_head.png",
                Cost = 8,
                Type = SlotType.Head,
                MaxQuantity = 1,
            }; context.Items.Add(tint7_head);

            var tint7_leg = new ItemInfo()
            {
                Name = "tint7_leg.png",
                Cost = 8,
                Type = SlotType.Leg,
                MaxQuantity = 1,
            }; context.Items.Add(tint7_leg);

            var tint7_neck = new ItemInfo()
            {
                Name = "tint7_neck.png",
                Cost = 8,
                Type = SlotType.Neck,
                MaxQuantity = 1,
            }; context.Items.Add(tint7_neck);

            var tint8_arm = new ItemInfo()
            {
                Name = "tint8_arm.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(tint8_arm);

            var tint8_hand = new ItemInfo()
            {
                Name = "tint8_hand.png",
                Cost = 8,
                Type = SlotType.Hand,
                MaxQuantity = 1,
            }; context.Items.Add(tint8_hand);

            var tint8_head = new ItemInfo()
            {
                Name = "tint8_head.png",
                Cost = 8,
                Type = SlotType.Head,
                MaxQuantity = 1,
            }; context.Items.Add(tint8_head);

            var tint8_leg = new ItemInfo()
            {
                Name = "tint8_leg.png",
                Cost = 8,
                Type = SlotType.Leg,
                MaxQuantity = 1,
            }; context.Items.Add(tint8_leg);

            var tint8_neck = new ItemInfo()
            {
                Name = "tint8_neck.png",
                Cost = 8,
                Type = SlotType.Neck,
                MaxQuantity = 1,
            }; context.Items.Add(tint8_neck);


            #endregion

            // mock guilds

            var guild1 = new Guild() 
            { 
                Name = "Abyss Watchers" 
            }; context.Guilds.Add(guild1);

            var guild2 = new Guild()
            {
                Name = "Warriors of Sunlight"
            }; context.Guilds.Add(guild2);

            var guild3 = new Guild()
            {
                Name = "Blades of the Darkmoon"
            }; context.Guilds.Add(guild3);

            var guild4 = new Guild()
            {
                Name = "Wisdom Magicians "
            }; context.Guilds.Add(guild4);

            var guild5 = new Guild()
            {
                Name = "Chaos Servants"
            }; context.Guilds.Add(guild5);

            // mock characters
            var char1 = new Character()
            {
                Name = "Gandalf",
                TotalXP = 5,
                Gold = 3000,
                HealthPoints = 100,
            }; context.Characters.Add(char1);

            char1.Items.Add(new InventoryItem(blackShoe2, 1, isEquipped: true));
            char1.Items.Add(new InventoryItem(tint3_hand, 1, isEquipped: true));
            char1.Items.Add(new InventoryItem(tint5_neck, 1, isEquipped: true));
            char1.Items.Add(new InventoryItem(armYellow_long, 1, isEquipped: true));
            char1.Items.Add(new InventoryItem(pantsGreen_long, 1, isEquipped: true));

            char1.Guild = guild1;

            var char2 = new Character()
            {
                Name = "Jurema",
                TotalXP = 55,
                Gold = 3000,
                HealthPoints = 80,
            }; context.Characters.Add(char2);

            char2.Items.Add(new InventoryItem(blueShoe5, 1, isEquipped: true));
            char2.Items.Add(new InventoryItem(tint6_hand, 1, isEquipped: true));
            char2.Items.Add(new InventoryItem(tint8_neck, 1, isEquipped: true));
            char2.Items.Add(new InventoryItem(armWhite_long, 1, isEquipped: true));
            char2.Items.Add(new InventoryItem(pantsLightBlue_long, 1, isEquipped: true));

            char2.Guild = guild2;

            // mock quizzes
            var quiz1 = new Quiz()
            {
                Title = "Quiz 1 Title",
                Owner = char1,
            };

            var quiz2 = new Quiz()
            {
                Title = "Quiz 2 Title",
                Owner = char2,
            };

            var question1 = new Question()
            {
                Text = "What is the answer to the meaning of life, the universe and everything?",
                Answers = new List<Answer>()
                {
                    new Answer()
                    {
                        IsCorrect = false,
                        Text = "40",
                    },

                    new Answer()
                    {
                        IsCorrect = false,
                        Text = "41",
                    },

                    new Answer()
                    {
                        IsCorrect = true,
                        Text = "42",
                    },

                    new Answer()
                    {
                        IsCorrect = false,
                        Text = "43",
                    },
                }
            };

            var question2 = new Question()
            {
                Text = "This is a true or false question. True or False?",
                Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                IsCorrect = true,
                                Text = "True",
                            },

                            new Answer()
                            {
                                IsCorrect = false,
                                Text = "False",
                            },
                        }
            };

            var question3 = new Question()
            {
                Text = "All options are correct. Which options are correct?",
                Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                IsCorrect = true,
                                Text = "A",
                            },

                            new Answer()
                            {
                                IsCorrect = true,
                                Text = "B",
                            },
                        }
            };

            quiz1.Questions.Add(question1);

            quiz2.Questions.Add(question2);
            quiz2.Questions.Add(question3);

            context.Quizzes.Add(quiz1);
            context.Quizzes.Add(quiz2);
        }
    }
}
