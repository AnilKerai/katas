namespace bowling_score_calc;

using System;
using System.Collections.Generic;

public class BowlingScoreCalculator
{ 
    private const double STRIKE_MOD = 2.0;
    private const double SPARE_MOD = 1.5;
    private List<string> ALLOWED_VALUES = new() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "X", "/" };

    public double Calculate(string scores)
    {
        if (scores.Length > 10)
            throw new ArgumentException("cannot have more than 10 rounds");

        var total = 0.0;
        var isStrike = false;
        var isSpare = false;
        var scoreModifier = 0.0;

        foreach (var s in scores)
        {
            var score = s.ToString().ToUpper();
            ValidateScoreInput(score);
            isStrike = score.Equals("X");
            isSpare = score.Equals("/");

            if (isStrike)
            {
                scoreModifier += STRIKE_MOD;
                continue;
            }

            if (isSpare)
            {
                scoreModifier += SPARE_MOD;
                continue;
            }

            var calculatedScoreForRound = double.Parse(score);
            
            if (scoreModifier > 0.0)
            {
                calculatedScoreForRound = Math.Ceiling(calculatedScoreForRound * scoreModifier);
                scoreModifier = 0.0;
            }

            total += calculatedScoreForRound;
        }

        if (total != 0.0) return total;
        if (isStrike)
        {
            total = 10 * 2.0 * 2.0 * 2.0 * 2.0 * 2.0 * 2.0 * 2.0 * 2.0 * 2.0 * 2.0;
        }

        if (isSpare)
        {
            total = Math.Ceiling(5 * 1.5 * 1.5 * 1.5 * 1.5 * 1.5 * 1.5 * 1.5 * 1.5 * 1.5 * 1.5);
        }

        return total;
    }

    private void ValidateScoreInput(string score)
    {
        var isValid = ALLOWED_VALUES.Contains(score);
        if (!isValid)
            throw new ArgumentException($"Invalid score. Value '{score}' is not allowed.");
    }
}