'use client';
import { useParamsStore } from "@/hooks/useParameterStore";
import { useRouter,usePathname } from "next/navigation";
import { IoMdMan } from "react-icons/io"


export const Logo =()=>{
    const reset= useParamsStore(state => state.reset);
    const router = useRouter();
    const pathname = usePathname();

    function doReset() {
        if (pathname !== '/') router.push('/');
        reset();
    }
return (
     <div onClick={doReset} className="cursor-pointer flex items-center gap-2 text-3xl font-semibold text-red-200">
      <IoMdMan size={30}/>
       Logo
    </div>
)
}