/// <reference types="multer" />
import { CreateUserDto } from "src/dto/createUser.dto";
import { LogginUserDto } from "src/dto/logginUser.dto";
import { AuthorizationService } from "./authorization.service";
export declare class AuthorizationController {
    private authorizationService;
    constructor(authorizationService: AuthorizationService);
    registration(createUserDto: CreateUserDto, avatar: Express.Multer.File): Promise<string>;
    loggin(logginUserDto: LogginUserDto): Promise<string>;
}
