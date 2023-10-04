﻿const slider = document.getElementsByClassName('slider')

for (let sliderElement of slider) {
    const slideCol = sliderElement.getElementsByClassName('slide');
    const lastSlide = slideCol.item(0);
    const cloneLast = lastSlide.cloneNode(true);
    sliderElement.append(cloneLast)
    slideAction(sliderElement, slideCol.length)
}


function slideAction(slider, count) {
    let index = 0
    let trans = slider.style.transition
    let t1 = setInterval(() => {
        slider.style.transform = `translateX(-${index % count * 100}%)`
        if (index === count - 1) {
            setTimeout(() => {
                slider.style.transition = ""
            }, 300)
            setTimeout(() => {
                slider.style.transform = `translateX(0)`

            }, 320)
            setTimeout(() => {
                slider.style.transition = trans;
            }, 340)
            index = 0
        }
        index++
    }, 4000)
}