export  type  PagedResult<T> = {
    results : T[],
    pageCount : number,
    totalCount : number
}

export interface Auction {
    reservePrice: number
    sellerId: string
    winnerId?: string
    soldAmount: any
    currentHighestBid: any
    createdAt: string
    updatedAt: string
    auctionEnd: string
    status: string
    make: string
    model: string
    year: number
    color: string
    mileage: number
    imageUrl: string
    id: string
  }