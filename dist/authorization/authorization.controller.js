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
exports.AuthorizationController = void 0;
const common_1 = require("@nestjs/common");
const platform_express_1 = require("@nestjs/platform-express");
const createUser_dto_1 = require("../dto/createUser.dto");
const logginUser_dto_1 = require("../dto/logginUser.dto");
const authorization_service_1 = require("./authorization.service");
let AuthorizationController = class AuthorizationController {
    constructor(authorizationService) {
        this.authorizationService = authorizationService;
    }
    ;
    registration(createUserDto, avatar) {
        return this.authorizationService.registration(createUserDto, avatar);
    }
    async loggin(logginUserDto) {
        return this.authorizationService.loggin(logginUserDto);
    }
};
__decorate([
    (0, common_1.Post)('/register'),
    (0, common_1.UseInterceptors)((0, platform_express_1.FileInterceptor)('avatar')),
    __param(0, (0, common_1.Body)()),
    __param(1, (0, common_1.UploadedFile)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [createUser_dto_1.CreateUserDto, Object]),
    __metadata("design:returntype", Promise)
], AuthorizationController.prototype, "registration", null);
__decorate([
    (0, common_1.Post)('/loggin'),
    __param(0, (0, common_1.Body)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [logginUser_dto_1.LogginUserDto]),
    __metadata("design:returntype", Promise)
], AuthorizationController.prototype, "loggin", null);
AuthorizationController = __decorate([
    (0, common_1.Controller)('/auth'),
    __metadata("design:paramtypes", [authorization_service_1.AuthorizationService])
], AuthorizationController);
exports.AuthorizationController = AuthorizationController;
//# sourceMappingURL=authorization.controller.js.map