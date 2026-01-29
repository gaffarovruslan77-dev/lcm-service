using System.Numerics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

BigInteger Gcd(BigInteger a, BigInteger b) 
{ 
    while (b != 0) 
    { 
        BigInteger temp = b; 
        b = a % b; 
        a = temp; 
    } 
    return a; 
}

BigInteger Lcm(BigInteger x, BigInteger y) 
{ 
    return (x / Gcd(x, y)) * y;
}

bool IsNaturalNumber(string? value) 
{
    if (string.IsNullOrWhiteSpace(value)) return false;
    if (!BigInteger.TryParse(value.Trim(), out BigInteger number)) return false;
    if (number <= 0) return false;
    if (value.Trim() != number.ToString()) return false;
    return true;
}

app.MapGet("/gaffarovruslan77_gmail_com", (HttpContext context) => 
{
    string? xParam = context.Request.Query["x"].ToString();
    string? yParam = context.Request.Query["y"].ToString();
    
    if (!IsNaturalNumber(xParam) || !IsNaturalNumber(yParam)) 
    {
        context.Response.ContentType = "text/plain; charset=utf-8";
        return Results.Text("NaN");
    }
    
    BigInteger x = BigInteger.Parse(xParam!.Trim());
    BigInteger y = BigInteger.Parse(yParam!.Trim());
    BigInteger result = Lcm(x, y);
    
    context.Response.ContentType = "text/plain; charset=utf-8";
    return Results.Text(result.ToString());
});

app.Run();
