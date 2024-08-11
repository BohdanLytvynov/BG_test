import { ElementRef } from "@angular/core";

export abstract class ValidatorBase
{
    validArray : boolean[] = [];  

    constructor(count : number)
    {
        for (let i = 0; i < count; i++)
            {
                this.validArray.push(false);
            }
    }

  CheckValidArray() : boolean
  {
    for(let i = 0; i < this.validArray.length; i++)
      if(!this.validArray[i]) return false;
    return true;  
  }

  disableElement(value : boolean, submit : ElementRef)
  {    
    if(value)
    {
      submit.nativeElement.disabled = true;    
    }

  }

}