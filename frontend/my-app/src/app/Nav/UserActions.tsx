'use client';


import { Dropdown, DropdownDivider, DropdownItem } from "flowbite-react"
import Link from "next/link"
import { useRouter } from "next/navigation"
import { User } from 'next-auth'
import { signOut } from 'next-auth/react'
import { AiFillCar, AiFillTrophy, AiOutlineLogout } from 'react-icons/ai'
import {HiCog, HiUser} from 'react-icons/hi2';

type Props = {
  user: User
}

export  const UserActions = ({user}: Props) => {
  const router = useRouter();
  return (
    <>
    <Dropdown
      inline
      label={`Welcome ${user.name}`}
    >
      <DropdownItem icon={HiUser}>
          My Auctions
      </DropdownItem>
      <DropdownItem icon={AiFillTrophy}>
          Auctions won
      </DropdownItem>
      <DropdownItem icon={AiFillCar}>
        <Link href='/auctions/create'>
          Sell my car
        </Link>
      </DropdownItem>
      <DropdownItem icon={HiCog}>
        <Link href='/session'>
          Session (dev only)
        </Link>
      </DropdownItem>
      <DropdownDivider />
      <DropdownItem icon={AiOutlineLogout} onClick={() => signOut({callbackUrl: '/'})}>
        Sign out
      </DropdownItem>
    </Dropdown>
    </>
  )
}