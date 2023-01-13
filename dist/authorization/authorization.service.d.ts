import { Model } from "mongoose";
import { CreateUserDto } from "src/dto/createUser.dto";
import { LogginUserDto } from "src/dto/logginUser.dto";
import { User, UserDocument } from "src/schemas/user.schema";
export declare class AuthorizationService {
    private userModel;
    constructor(userModel: Model<UserDocument>);
    registration(createUserDto: CreateUserDto): Promise<User>;
    loggin(logginUserDto: LogginUserDto): Promise<User>;
}
