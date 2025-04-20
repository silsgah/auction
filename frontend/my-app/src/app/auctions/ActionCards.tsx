
import { CountDownTimer } from './CountDownTimer';
import { CarImages } from './CarImages';
import { Auction } from '@/types/type';

type Props = {
    auction: Auction
}
export  const AuctionCards =  ({ auction }: Props) =>{
  return (
    <a href="" className='group'>
      <div className="relative w-full bg-gray-200 aspect-[16/10] rounded-lg overflow-hidden">
      <CarImages imageUrl={auction.imageUrl} make={auction.make}/>
      <div className='absolute bottom-2 left-2'>
      <CountDownTimer auctionEnd={auction.auctionEnd} />
      </div>
      </div>
      <div className='flex justify-between items-center mt-4'>
        <h3 className='text-gray-400'>{auction.make} {auction.model}</h3>
        <p className='font-semibold text-sm'>{auction.year}</p>
      </div>
      
    </a>
  )
}