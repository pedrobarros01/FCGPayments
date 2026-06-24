using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Exceptions;

public class BusinessException(string message) : global::System.Exception(message)
{
}
