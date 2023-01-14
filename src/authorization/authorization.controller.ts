import { Body, Controller, Post, UploadedFile, UseInterceptors } from "@nestjs/common";
import { FileInterceptor } from "@nestjs/platform-express";
import { CreateUserDto } from "src/dto/createUser.dto";
import { LogginUserDto } from "src/dto/logginUser.dto";
import { AuthorizationService } from "./authorization.service";
@Controller('/auth')
export class AuthorizationController{
    constructor(private authorizationService:AuthorizationService){};
    @Post('/register')
    @UseInterceptors(FileInterceptor('avatar'))
    registration(@Body() createUserDto:CreateUserDto, @UploadedFile() avatar:Express.Multer.File):Promise<string>{
        return this.authorizationService.registration(createUserDto, avatar);
    }
    @Post('/loggin')
    async loggin(@Body() logginUserDto:LogginUserDto):Promise<string>{
        return this.authorizationService.loggin(logginUserDto);
    }
}