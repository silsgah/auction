'use client';
import Countdown, { zeroPad } from "react-countdown";

type DateProps = {
    days: number,
    hours: number,
    minutes: number,
    seconds: number,
    completed: boolean
}

type Props = {
    auctionEnd: string
}
const renderer = ({days, hours, minutes, seconds, completed } : DateProps ) => {
   return (
    <div className={`border-2 border-white text-white py-1 px-2 rounded-lg flex justify-center 
                    ${completed ? `bg-red-600` : (days=== 0 && hours < 10)  ? `bg-amber-600` : 
                  `bg-green-600`}
                    `}>
       {completed ? (<span>Auction Finished</span>) : (
        <span suppressHydrationWarning={true}>
          {zeroPad(days)}:{zeroPad(hours)}:{zeroPad(minutes)}:{seconds}
          </span>
     )}

    </div>
   )
    if (completed) {
      return <span>Finsied</span>;
    } else {
      return <span>{days}:{hours}:{minutes}:{seconds}</span>;
    }
  };

export  const CountDownTimer =  ({auctionEnd}: Props) => { 
  return (
    <div>
       <Countdown date={auctionEnd} renderer={renderer} />
    </div>
  )
}