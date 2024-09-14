import { JwtPayload } from "jwt-decode";
export interface LoginCredentials {
    email: string;
    password: string;
  }
  
  export interface RegisterData {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
  }
  
  export interface AuthResponse {
    id: string;
    firstName: string;
    lastName: string;
    avatarUrl: string;
    token: string;
  }

  export interface JwtInformation extends JwtPayload {
    given_name: string;
    family_name: string;
  }