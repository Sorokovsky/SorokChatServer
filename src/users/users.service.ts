import { HttpException, HttpStatus, Injectable } from "@nestjs/common";
import { InjectModel, Prop, SchemaFactory } from "@nestjs/mongoose";
import { Model } from "mongoose";
import { User, UserDocument } from "src/schemas/user.schema";
import { CreateUserDto } from "../dto/createUser.dto";
@Injectable()
export class UsersService {
    constructor(@InjectModel(User.name) private userModel: Model<UserDocument>) {}
    async getAll(){
        return await this.userModel.find();
    }
   async getOneById( id:string):Promise<User>{
        try {
            const user = await this.userModel.findById(id);
            return user;
        } catch (error) {
            throw new HttpException("User not founded", HttpStatus.BAD_REQUEST);
        }
    }
    filterUsers():string{
        return `filter`;
    }
    async create(createUserDto:CreateUserDto):Promise<User>{
        try {
            const user = await this.userModel.create(createUserDto);
            console.log(user);
            
            return user;
        } catch (error) {
            throw new HttpException(error.message, 500);
        }
    }
    delete(id:string):string{
        return `Deleted ${id}`;
    }
}