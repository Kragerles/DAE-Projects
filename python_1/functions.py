def addNumbers(n1 , n2):
    print(int(n1)+int(n2))

def subNumbers(n1 , n2):
    print(int(n1)-int(n2))

def multNumbers(n1 , n2):
    print(int(n1)*int(n2))

def divNumbers(n1 , n2):
    print(int(n1)/int(n2))

def disMenu():
    n1 = input("Input first number : ")
    n2 = input("Input second number : ")
    choice = input("Input desired function : + , - , * , / : ")
    if choice == "+":
        addNumbers(n1 , n2)
    elif choice == "-":
        subNumbers(n1 , n2)
    elif choice == "*":
        multNumbers(n1 , n2)
    elif choice == "/":
        divNumbers(n1 , n2)

def main():
    n1 = 0
    n2 = 0
    choice = ""
    disMenu()
    print(choice)
    

main()