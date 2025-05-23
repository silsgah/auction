import { EmptyFilter } from "@/components/EmptyFilter";


export default function SignIn({searchParams}: {searchParams: {callbackUrl: string}}) {
    return (
      <EmptyFilter 
          title='You need to be logged in to do that'
          subtitle='Please click below to sign in'
          showLogin
          callbackUrl={searchParams.callbackUrl}
      />
    )
  }
  