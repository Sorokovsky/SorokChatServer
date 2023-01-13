import { Module } from "@nestjs/common";
import { MongooseModule } from "@nestjs/mongoose";
import { FileService } from "src/file/file.service";
import { User, UserSchema } from "src/schemas/user.schema";
import { AuthorizationController } from "./authorization.controller";
import { AuthorizationService } from "./authorization.service";
@Module({
   controllers: [AuthorizationController],
   providers: [AuthorizationService, FileService],
   imports: [MongooseModule.forFeature([{schema: UserSchema, name: User.name}])]
})
export class AuthorizationModule{

}