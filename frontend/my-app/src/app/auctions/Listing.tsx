'use client'

import { Auction, PagedResult } from "@/types/type";
import { AuctionCards } from "./ActionCards";
import { useEffect, useState } from "react";
import { getListing } from "../actions/auctionActions";
import { Filters } from "./Filters";
import { AppPagination } from "./AppPagination";

import { useShallow } from "zustand/shallow";
import qs from 'query-string';

import { useParamsStore } from "@/hooks/useParameterStore";
import { EmptyFilter } from "@/components/EmptyFilter";


export  const Listing = () =>{
  const [data, setData] = useState<PagedResult<Auction>>();
  const params = useParamsStore(useShallow(state=>({
      pageNumber: state.pageNumber,
      pageSize: state.pageSize,
      searchTerm: state.searchTerm,
      orderBy: state.orderBy,
      filterBy: state.filterBy
  })));
  const setParams = useParamsStore(state => state.setParams);
  const url = qs.stringify({...params});
  function setPageNumber(pageNumber: number){
    setParams({pageNumber})
  }


  useEffect(() => {
  getListing(url).then(data => {
    setData(data)
  });
}, [url]); 


  if(!data) {
    return <h3>loading...</h3>
  }
  if(data.totalCount ===0) return <EmptyFilter showReset/>
  return (
    <>
    <Filters />
    {data.totalCount ===0?(
      <EmptyFilter showReset/>
    ) : (
   <>  
    <div className="grid grid-cols-4 gap-6">
      {data.results.map((auction: any)=>(
          <AuctionCards auction={auction} key={auction.id}/>
      ))}
    </div>
    <div className="flex justify-center mt-4">
    <AppPagination 
      pageChanged={setPageNumber} 
      currentPage={params.pageNumber}
      pageCount={data.pageCount}
    />
    </div> 
  </>
    )}
   
    </>
  )
}
