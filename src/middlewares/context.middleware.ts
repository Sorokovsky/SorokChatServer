import { NestMiddleware } from "@nestjs/common";
import { NextFunction } from "express";
import { storage } from "src/main";
import { Context } from '../utils/context-storage';

export class ContextMiddleware implements NestMiddleware {
    use(req: Express.Request, res: Express.Response, next: NextFunction) {
        storage.run({ request: req, response: res } as Context, () => next());
    }
};