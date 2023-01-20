import { Body, Controller, Delete, Get, Param, Post } from "@nestjs/common";
import { User } from "src/schemas/user.schema";
import { CreateUserDto } from "../dto/createUser.dto";
import { UsersService } from "./users.service";
@Controller('/users')
export class UsersController{
    constructor(private usersService:UsersService){}
    @Get()
    getAll(){
        return this.usersService.getAll();
    }
    @Get('/:id')
    getOneById(@Param('id') id:string):Promise<User>{
        return this.usersService.getOneById(id);
    }
    @Post('/filter')
    filterUsers():string{
        return this.usersService.filterUsers();
    }
    @Post()
    create(@Body() createUserDto:CreateUserDto):Promise<User>{
        return this.usersService.create(createUserDto);
    }
    @Delete("/:id")
    delete(@Param('id') id:string):string{
        return this.usersService.delete(id);
    }
}