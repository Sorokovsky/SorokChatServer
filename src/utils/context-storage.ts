import { Request, Response } from "express";
import { storage } from "../main";

export type Context = {
    request: Request;
    response: Response;
};
export const getContext = async (): Promise<Context> => {
    return await storage.getStore() as Promise<Context>;
};