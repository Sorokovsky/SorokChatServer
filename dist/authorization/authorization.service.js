"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.AuthorizationService = void 0;
const common_1 = require("@nestjs/common");
const mongoose_1 = require("@nestjs/mongoose");
const mongoose_2 = require("mongoose");
const user_schema_1 = require("../schemas/user.schema");
const bcrypt_1 = require("bcrypt");
const dotenv_1 = require("dotenv");
const file_service_1 = require("../file/file.service");
const jwt = require("jsonwebtoken");
(0, dotenv_1.config)();
const salt = Number(process.env.SALT) || 6;
let AuthorizationService = class AuthorizationService {
    constructor(userModel, fileService) {
        this.userModel = userModel;
        this.fileService = fileService;
    }
    ;
    async registration(createUserDto, avatar) {
        try {
            const hashedPassword = await (0, bcrypt_1.hash)(createUserDto.password, salt);
            let user = await this.userModel.create(Object.assign(Object.assign({}, createUserDto), { password: hashedPassword }));
            if (avatar) {
                const avatarPath = await this.fileService.uploadAvatar(avatar, user._id);
                user.avatar = avatarPath;
                await user.save();
            }
            return jwt.sign(Object.assign({}, user), process.env.SECRET_KEY, { expiresIn: 6000 * 60 * 24 });
        }
        catch (error) {
            throw new common_1.HttpException(error.message, common_1.HttpStatus.BAD_REQUEST);
        }
    }
    async loggin(logginUserDto) {
        try {
            const candidate = await this.userModel.findOne({ email: logginUserDto.email });
            if (!candidate)
                throw new Error('User not founded');
            if (await (0, bcrypt_1.compare)(logginUserDto.password, candidate.password)) {
                return jwt.sign(Object.assign({}, candidate), process.env.SECRET_KEY, { expiresIn: "20d" });
            }
            throw new Error('Do not correct the password');
        }
        catch (error) {
            throw new common_1.HttpException(error.message, common_1.HttpStatus.BAD_REQUEST);
        }
    }
};
AuthorizationService = __decorate([
    (0, common_1.Injectable)(),
    __param(0, (0, mongoose_1.InjectModel)(user_schema_1.User.name)),
    __metadata("design:paramtypes", [mongoose_2.Model, file_service_1.FileService])
], AuthorizationService);
exports.AuthorizationService = AuthorizationService;
//# sourceMappingURL=authorization.service.js.map