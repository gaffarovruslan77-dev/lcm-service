var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

long Gcd(long a, long b) 
{ 
    while (b != 0) 
    { 
        long temp = b; 
        b = a % b; 
        a = temp; 
    } 
    return a; 
}

long Lcm(long x, long y) 
{ 
    return (x / Gcd(x, y)) * y;
}

bool IsNaturalNumber(string? value) 
{
    if (string.IsNullOrWhiteSpace(value)) return false;
    if (!long.TryParse(value.Trim(), out long number)) return false;
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
    
    long x = long.Parse(xParam!.Trim());
    long y = long.Parse(yParam!.Trim());
    long result = Lcm(x, y);
    
    context.Response.ContentType = "text/plain; charset=utf-8";
    return Results.Text(result.ToString());
});

app.Run();
