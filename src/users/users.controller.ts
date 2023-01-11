import { Controller, Get, Param, Post } from "@nestjs/common";
@Controller('/users')
export class UsersController{
    @Get()
    getAll():string{
        return 'All Users';
    }
    @Get('/:id')
    getOneById(@Param('id') id:string):string{
        return id;
    }
    @Post('/filter')
    filterUsers():string{
        return `filter`;
    }
    @Post()
    create():string{
        return "created";
    }
}