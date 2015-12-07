using System;
using System.Collections.Generic;
using System.Text;

public class StringCompute
{

    private char[] _ArrChar1;
    private char[] _ArrChar2;
    private Result _Result;
    private DateTime _BeginTime;
    private DateTime _EndTime;
    private int _ComputeTimes;
    private int[,] _Matrix;
    private int _Column;
    private int _Row;

    public Result ComputeResult
    {
        get { return _Result; }
    }

    public StringCompute(string str1, string str2)
    {
        this.StringComputeInit(str1, str2);
    }
    public StringCompute()
    {
    }

    private void StringComputeInit(string str1, string str2)
    {
        _ArrChar1 = str1.ToCharArray();
        _ArrChar2 = str2.ToCharArray();
        _Result = new Result();
        _ComputeTimes = 0;
        _Row = _ArrChar1.Length + 1;
        _Column = _ArrChar2.Length + 1;
        _Matrix = new int[_Row, _Column];
    }

    public void Compute()
    {
        _BeginTime = DateTime.Now;
        this.InitMatrix();
        int intCost = 0;
        for (int i = 1; i < _Row; i++)
        {
            for (int j = 1; j < _Column; j++)
            {
                if (_ArrChar1[i - 1] == _ArrChar2[j - 1])
                {
                    intCost = 0;
                }
                else
                {
                    intCost = 1;
                }
                //the last _Matrix[_Row - 1, _Column - 1]is the distance between two strings
                _Matrix[i, j] = this.Minimum(_Matrix[i - 1, j] + 1, _Matrix[i, j - 1] + 1, _Matrix[i - 1, j - 1] + intCost);
                _ComputeTimes++;
            }
        }
        _EndTime = DateTime.Now;
        //similarity rate
        int intLength = _Row > _Column ? _Row : _Column;

        _Result.Rate = (1 - (decimal)_Matrix[_Row - 1, _Column - 1] / intLength);
        _Result.UseTime = (_EndTime - _BeginTime).ToString();
        _Result.ComputeTimes = _ComputeTimes.ToString();
        _Result.Difference = _Matrix[_Row - 1, _Column - 1];
    }

    public void SpeedyCompute()
    {
        this.InitMatrix();
        int intCost = 0;
        for (int i = 1; i < _Row; i++)
        {
            for (int j = 1; j < _Column; j++)
            {
                if (_ArrChar1[i - 1] == _ArrChar2[j - 1])
                {
                    intCost = 0;
                }
                else
                {
                    intCost = 1;
                }
                _Matrix[i, j] = this.Minimum(_Matrix[i - 1, j] + 1, _Matrix[i, j - 1] + 1, _Matrix[i - 1, j - 1] + intCost);
                _ComputeTimes++;
            }
        }
        //_EndTime = DateTime.Now;
        int intLength = _Row > _Column ? _Row : _Column;

        _Result.Rate = (1 - (decimal)_Matrix[_Row - 1, _Column - 1] / intLength);
        // _Result.UseTime = (_EndTime - _BeginTime).ToString();
        _Result.ComputeTimes = _ComputeTimes.ToString();
        _Result.Difference = _Matrix[_Row - 1, _Column - 1];
    }

    public void Compute(string str1, string str2)
    {
        this.StringComputeInit(str1, str2);
        this.Compute();
    }

    public void SpeedyCompute(string str1, string str2)
    {
        this.StringComputeInit(str1, str2);
        this.SpeedyCompute();
    }
    private void InitMatrix()
    {
        for (int i = 0; i < _Column; i++)
        {
            _Matrix[0, i] = i;
        }
        for (int i = 0; i < _Row; i++)
        {
            _Matrix[i, 0] = i;
        }
    }

    private int Minimum(int First, int Second, int Third)
    {
        int intMin = First;
        if (Second < intMin)
        {
            intMin = Second;
        }
        if (Third < intMin)
        {
            intMin = Third;
        }
        return intMin;
    }
}

public struct Result
{

    public decimal Rate;
    public string ComputeTimes;
    public string UseTime;
    public int Difference;
}