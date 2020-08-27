using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LogicResult
{
    public bool IsSuccess { get; set; }

    public string message { get; set; }
}
public class LogicResult<TResult> : LogicResult where TResult: class
{
    public TResult Result { get; set; }
}
