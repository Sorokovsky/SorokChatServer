import { Module } from "@nestjs/common";
import { MongooseModule } from "@nestjs/mongoose";
import { User, UserSchema } from "src/schemas/user.schema";
import { AuthorizationController } from "./authorization.controller";
import { AuthorizationService } from "./authorization.service";
@Module({
   controllers: [AuthorizationController],
   providers: [AuthorizationService],
   imports: [MongooseModule.forFeature([{schema: UserSchema, name: User.name}])]
})
export class AuthorizationModule{

}