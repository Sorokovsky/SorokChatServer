import { User } from "src/database/entities/user.entity";
import { NotFoundException } from "../base/not-found.exception";

export class UserNotFoundException extends NotFoundException<User> {
    constructor(key: keyof User, value: any) {
        super("Користувача", key, value);
    };
};