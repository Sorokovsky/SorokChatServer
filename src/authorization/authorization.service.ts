import { HttpException, HttpStatus, Injectable } from "@nestjs/common";
import { InjectModel } from "@nestjs/mongoose";
import { Model } from "mongoose";
import { CreateUserDto } from "src/dto/createUser.dto";
import { LogginUserDto } from "src/dto/logginUser.dto";
import { User, UserDocument } from "src/schemas/user.schema";
import { hash, compare } from 'bcrypt';
import { config } from 'dotenv';
config();
const salt:number = Number(process.env.SALT) || 6;
@Injectable()
export class AuthorizationService{
    constructor(@InjectModel(User.name) private userModel: Model<UserDocument>){};
    async registration(createUserDto:CreateUserDto):Promise<User>{
        try {
            const hashedPassword:string = await hash(createUserDto.password, salt);
            const user = await this.userModel.create({...createUserDto, password: hashedPassword});
            return user;
        } catch (error) {
            throw new HttpException(error.message, HttpStatus.BAD_REQUEST);
        }
    }
    async loggin(logginUserDto:LogginUserDto):Promise<User>{
        try {
            const candidate = await this.userModel.findOne({email:logginUserDto.email});
            if (!candidate) throw new Error('User not founded');
            if (await compare(logginUserDto.password, candidate.password)) return candidate;
            throw new Error('Do not correct the password');
        } catch (error) {
            throw new HttpException(error.message, HttpStatus.BAD_REQUEST);
        }
    }
}