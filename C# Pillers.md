1.Pillars of C# (OOP Pillars)

   a) Encapsulation (ডাটা সুরক্ষা)
   ডাটা আর তার কাজ একসাথে রাখা

   <img width="762" height="369" alt="Screenshot 2025-12-13 121143" src="https://github.com/user-attachments/assets/44229d47-9ef9-4856-8468-96f60f2655e1" />

  ভেতরের জিনিস বাইরে থেকে নষ্ট করা যাবে না

  b) Inheritance (উত্তরাধিকার) 
  একটা class আরেকটা class-এর গুণ নেয়

  <img width="756" height="292" alt="image" src="https://github.com/user-attachments/assets/17685b1c-2c27-4214-b6cf-c1e0a5a73a48" />

  Dog, Animal-এর সব কাজ ব্যবহার করতে পারে

  c) Polymorphism (এক জিনিস, অনেক রূপ)
  একই method, ভিন্ন আচরণ

  <img width="749" height="375" alt="image" src="https://github.com/user-attachments/assets/658c55f9-acf2-45f7-9f97-e6b652391290" />
  
  Sound() একই, কিন্তু কাজ আলাদা

  d) Abstraction (প্রয়োজনীয় অংশ দেখানো)
  ভেতরের জটিলতা লুকানো

  <img width="762" height="312" alt="image" src="https://github.com/user-attachments/assets/925e826c-34e9-4b94-9f96-28efee2d3d96" />

  কী করবে জানি, কিভাবে করবে জানি না

  2. Type Safety (টাইপ সেফটি)
  ভুল টাইপের ডাটা ব্যবহার করতে দেয় না
     
  <img width="564" height="75" alt="image" src="https://github.com/user-attachments/assets/2814451f-16d1-4f7d-83b6-9662db037f8a" />

  int এ string ঢুকাতে পারবে না,
  C# নিজেই ভুল ধরিয়ে দেয়,
  কম bug,
  নিরাপদ কোড,
  compile time error

3. async & await (সহজ ভাষায়)
সময় লাগা কাজের সময় program freeze না করা

<img width="706" height="141" alt="image" src="https://github.com/user-attachments/assets/b4a7a9ec-5d94-4ee9-aad8-372992d09481" />


তুমি খাবার অর্ডার দিলে
খাবার আসা পর্যন্ত বসে না থেকে অন্য কাজ করছো
খাবার এলে জানানো হলো,
async → কাজ শুরু
await → কাজ শেষ হওয়া পর্যন্ত অপেক্ষা


4. Memory Management (মেমোরি কিভাবে handle হয়)

<img width="501" height="75" alt="image" src="https://github.com/user-attachments/assets/7190537d-e056-4b62-bdb4-a000008e00aa" />

Garbage Collector (GC)

তুমি ময়লা ফেলো না, C# নিজেই পরিষ্কার করে
GC কখন কাজ করে?
মেমোরি কম হলে
program idle হলে












  
