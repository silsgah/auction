"use client";
import { Button } from "flowbite-react"
import { signIn } from "next-auth/react"

export const LoginButton =()=> {
 return (
    <div>
     <Button outline onClick={() => signIn('id-server', {redirectTo:'/'})}>
        Login
     </Button>
    </div>
 )
}