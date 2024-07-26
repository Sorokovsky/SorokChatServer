import { PickType } from "@nestjs/swagger";
import { User } from "src/database/entities/user.entity";
import type { UserKey } from "./key.type";

export const createUserKeys: UserKey[] = ['email', 'password'];
export class CreateUserDto extends PickType(User, createUserKeys) {};