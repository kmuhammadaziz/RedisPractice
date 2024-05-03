using Exercise2.Api.Dtos;
using Exercise2.Domain.Common.Entities;
using FastEndpoints;

namespace Exercise2.Api.Mappers;

public class MyResponseMapper : Mapper<MyRequest, MyResponse, Laptop>
{
    public override MyResponse FromEntity(Laptop e)
    {
        return new MyResponse()
        {
            Id = e.Id,
            UserName = e.UserName,
            Model = e.Model,
            Brand = e.Brand,
            Price = e.Price,
            DateOfIssue = e.DateOfIssue
        };
    }

    public override Laptop ToEntity(MyRequest r)
    {
        var id = Guid.NewGuid();

        return new Laptop()
        {
            Id = id,
            UserName = r.UserName,
            Model = r.Model,
            Brand = r.Brand,
            Price = r.Price,
            DateOfIssue = r.DateOfIssue
        };
    }
}