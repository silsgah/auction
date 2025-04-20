'use client';
import { Pagination } from "flowbite-react"
import { useState } from "react";


type PaginationProps = {
    currentPage : number
    pageCount: number
    pageChanged: (page: number) =>void;
}
export  const AppPagination =  ({currentPage, pageCount, pageChanged }: PaginationProps) =>{
    const [pageNumber, setPageNumber] = useState(currentPage);

  return (
    <Pagination
     currentPage={currentPage}
     onPageChange={e=>pageChanged(e)}
     totalPages={pageCount}
     layout="pagination"
     showIcons={true}
     className='text-gray-500 mb-5'
    />
  )
}