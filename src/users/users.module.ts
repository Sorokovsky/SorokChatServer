import { Module } from "@nestjs/common";
import { MongooseModule } from "@nestjs/mongoose";
import { User, UserSchema } from "src/schemas/user.schema";
import { UsersController } from "./users.controller";
import { UsersService } from "./users.service";
@Module({
    controllers: [UsersController],
    providers: [UsersService],
    imports: [MongooseModule.forFeature([{schema: UserSchema, name: User.name}])]
})
export class UsersModule{};