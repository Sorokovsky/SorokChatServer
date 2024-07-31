import { PickType } from "@nestjs/swagger";
import { User } from "src/database/entities/user.entity";
import { UserKey } from "../user/key.type";

export const loginKeys: UserKey[] = ['email', 'password'];
export class LoginDto extends PickType(User, loginKeys) { };