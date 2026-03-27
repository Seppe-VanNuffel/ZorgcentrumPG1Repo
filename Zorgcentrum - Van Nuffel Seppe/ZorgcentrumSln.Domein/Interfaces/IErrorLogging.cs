using System;
using System.Collections.Generic;
using System.Text;

namespace ZorgcentrumSln.Domein.Interfaces;

public interface IErrorLogging
{
    void LogError(Exception e);
}