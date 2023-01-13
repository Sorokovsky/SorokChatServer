import { Body, Controller, Post } from "@nestjs/common";
import { CreateUserDto } from "src/dto/createUser.dto";
import { LogginUserDto } from "src/dto/logginUser.dto";
import { User } from "src/schemas/user.schema";
import { AuthorizationService } from "./authorization.service";
@Controller('/auth')
export class AuthorizationController{
    constructor(private authorizationService:AuthorizationService){};
    @Post('/register')
    registration(@Body() createUserDto:CreateUserDto):Promise<User>{
        return this.authorizationService.registration(createUserDto);
    }
    @Post('/loggin')
    async loggin(@Body() logginUserDto:LogginUserDto):Promise<User>{
        return this.authorizationService.loggin(logginUserDto);
    }
}