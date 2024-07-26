import { OmitType } from "@nestjs/swagger";
import { User } from "src/database/entities/user.entity";
import { UserKey } from "./key.type";

export const omitedGetKeys: UserKey[] = ['password'];

export class GetUserDto extends OmitType(User, omitedGetKeys) {};