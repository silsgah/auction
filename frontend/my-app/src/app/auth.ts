import NextAuth, { Profile } from "next-auth"
import { OIDCConfig } from "next-auth/providers"
import DuendeIDS6Provider from "next-auth/providers/duende-identity-server6"


export const { handlers, signIn, signOut, auth } = NextAuth({
    session:{
        strategy:'jwt'
    },
  providers: [
    DuendeIDS6Provider({
        id:'id-server',
        clientId: "nextApp",
        clientSecret: "secret",
        issuer: "http://localhost:5001",
        authorization:{params: {scope:'openid profile auctionApp'}},
        idToken: true
      } as OIDCConfig<Omit<Profile, 'username'>>),
  ],
  callbacks: {
    async authorized({request, auth}){
      console.log("reguest" + request.body);
      return !!auth
    },
    async jwt({token, profile, account}) {
        if (profile) {
            token.username = profile.username
        }
        if (account) {
            token.access_token = account.access_token
        }
        return token;
    },
    async session({session, token}) {
        if (token) {
            session.user.username = token.username;
            session.accessToken = token.access_token;
        }
        return session;
    }
  }
})