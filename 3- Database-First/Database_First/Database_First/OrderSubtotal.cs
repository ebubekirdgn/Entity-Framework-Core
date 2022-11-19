using System;
using System.Collections.Generic;

namespace Database_First;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
