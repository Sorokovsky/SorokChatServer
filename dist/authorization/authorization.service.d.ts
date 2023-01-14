/// <reference types="multer" />
import { Model } from "mongoose";
import { CreateUserDto } from "src/dto/createUser.dto";
import { LogginUserDto } from "src/dto/logginUser.dto";
import { UserDocument } from "src/schemas/user.schema";
import { FileService } from "src/file/file.service";
export declare class AuthorizationService {
    private userModel;
    private fileService;
    constructor(userModel: Model<UserDocument>, fileService: FileService);
    registration(createUserDto: CreateUserDto, avatar: Express.Multer.File): Promise<string>;
    loggin(logginUserDto: LogginUserDto): Promise<string>;
}
