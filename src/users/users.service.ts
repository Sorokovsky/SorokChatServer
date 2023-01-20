import { HttpException, HttpStatus, Injectable } from "@nestjs/common";
import { InjectModel, } from "@nestjs/mongoose";
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
            const user = await this.userModel.findById(id).populate(['contacts', 'channels']);
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
            return user;
        } catch (error) {
            throw new HttpException(error.message, 500);
        }
    }
    async delete(id:string):Promise<User>{
        try {
            const deletedUser = await this.userModel.findByIdAndDelete(id);
            return deletedUser;
        } catch (error) {
            throw new HttpException(error.message, HttpStatus.BAD_REQUEST);
        }
    }
}