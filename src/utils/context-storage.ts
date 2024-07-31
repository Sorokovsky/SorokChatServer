import { storage } from "../main";

export type Context = {
    request: Express.Request;
    response: Express.Response;
};
export const getContext = async (): Promise<Context> => {
    return await storage.getStore() as Promise<Context>;
};