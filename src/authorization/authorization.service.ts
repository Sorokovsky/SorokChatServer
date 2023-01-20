import { HttpException, HttpStatus, Injectable } from "@nestjs/common";
import { InjectModel } from "@nestjs/mongoose";
import mongoose, { Model } from "mongoose";
import { CreateUserDto } from "src/dto/createUser.dto";
import { LogginUserDto } from "src/dto/logginUser.dto";
import { User, UserDocument } from "src/schemas/user.schema";
import { hash, compare } from 'bcrypt';
import { config } from 'dotenv';
import { FileService } from "src/file/file.service";
import * as jwt from 'jsonwebtoken';
config();
const salt:number = Number(process.env.SALT) || 6;
@Injectable()
export class AuthorizationService{
    constructor(@InjectModel(User.name) private userModel: Model<UserDocument>, private fileService:FileService){};
    async registration(createUserDto:CreateUserDto, avatar:Express.Multer.File):Promise<string>{
        try {
            const hashedPassword:string = await hash(createUserDto.password, salt);
            let user = await this.userModel.create({...createUserDto, password: hashedPassword});
            if(avatar){
                const avatarPath:string = await this.fileService.uploadAvatar(avatar, user._id);
                user.avatar = avatarPath;
                await user.save();
            }
            return jwt.sign({id: user._id}, process.env.SECRET_KEY, {expiresIn: "20d"});
        } catch (error) {
            throw new HttpException(error.message, HttpStatus.BAD_REQUEST);
        }
    }
    async loggin(logginUserDto:LogginUserDto):Promise<string>{
        try {
            const candidate = await this.userModel.findOne({email:logginUserDto.email});
            if (!candidate) throw new Error('User not founded');
            if (await compare(logginUserDto.password, candidate.password)) {
                return jwt.sign({id: candidate._id}, process.env.SECRET_KEY, {expiresIn: "20d"});
            }
            throw new Error('Do not correct the password');
        } catch (error) {
            throw new HttpException(error.message, HttpStatus.BAD_REQUEST);
        }
    }
}