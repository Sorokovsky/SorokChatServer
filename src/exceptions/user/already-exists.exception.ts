import { User } from "src/database/entities/user.entity";
import { AlreadyExistsException } from "../base/already-exists.exception";

export class UserAlreadyExistsException extends AlreadyExistsException<User> {
    constructor(key: keyof User, value: any) {
        super("Користувач", key, value);
    }
}