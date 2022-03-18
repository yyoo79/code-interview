/*
using System;
using System.DateTime;

// https://scriptbucket.wordpress.com/2012/01/01/token-bucket-algorithm-in-c/

public class TokenBucket_CSharp
{
    float _capacity = 0;
    float _tokens   = 0;
    float _fill_rate = 0;
    DateTime _time_stamp;
 
    public TokenBucket_CSharp(float tokens, float fill_rate)
    {
        _capacity  = tokens;
        _tokens    = tokens;
        _fill_rate = fill_rate;
        _time_stamp = DateTime.Now;
    }
 
    public bool Consume(float tokens)
    {
        if(tokens       {
            _tokens -= tokens;
        }else{
            return false;
        }
        return true;
    }
 
    public float GetTokens()
    {
        DateTime _now = DateTime.Now;
        if(_tokens < _capacity)
        {
            var delta = _fill_rate * (_now - _time_stamp);
            _tokens = Math.Min(_capacity, _tokens + delta);
            _time_stamp = _now;
        }
        return _tokens;
    }
}
*/