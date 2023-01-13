/// <reference types="multer" />
import { CreateUserDto } from "src/dto/createUser.dto";
import { LogginUserDto } from "src/dto/logginUser.dto";
import { User } from "src/schemas/user.schema";
import { AuthorizationService } from "./authorization.service";
export declare class AuthorizationController {
    private authorizationService;
    constructor(authorizationService: AuthorizationService);
    registration(createUserDto: CreateUserDto, avatar: Express.Multer.File): Promise<User>;
    loggin(logginUserDto: LogginUserDto): Promise<User>;
}
