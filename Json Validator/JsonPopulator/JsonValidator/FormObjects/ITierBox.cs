﻿using System.Windows.Forms;

namespace JsonValidator
{
    public interface ITierBox
    {
        void GeneratePrizeLine(FlowLayoutPanel flowPanel, string databasePath, Tier tier);
    }

    public interface IPrizeBox
    {
        string rewardType { get; set; }
        string rewardId { get; set; }
        int amount { get; set; }
        int instances { get; set; }
    };

    public interface IBoxGenerator
    {
       void Clear();
    }
}