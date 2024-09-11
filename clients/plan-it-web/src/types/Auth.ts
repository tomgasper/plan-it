import { JwtPayload } from "jwt-decode";
import { User } from "./User";

export interface LoginCredentials {
    email: string;
    password: string;
  }
  
  export interface RegisterData {
    username: string;
    email: string;
    password: string;
  }
  
  export interface AuthResponse {
    user: User;
    token: string;
  }

  export interface JwtInformation extends JwtPayload {
    given_name: string;
    family_name: string;
  }