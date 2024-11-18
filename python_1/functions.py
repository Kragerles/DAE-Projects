def addNumbers(n1 , n2):
    #Preforms addition
    print(int(n1)+int(n2))

def subNumbers(n1 , n2):
    #Preforms subtraction
    print(int(n1)-int(n2))

def multNumbers(n1 , n2):
    #Preforms multiplication
    print(int(n1)*int(n2))

def divNumbers(n1 , n2):
    #Preforms division
    print(int(n1)/int(n2))

def disMenu():
    #Displays UI and saves choice for algorithm
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
    allFeatures = ["Addition" , "Subtraction" , "Division" , "Multiplication"]
    n1 = 0
    n2 = 0
    choice = ""
    print("Can preform :")
    #Simple loop
    for currentFeature in allFeatures:print( currentFeature )
    print()
    #Complicated loop
    #for currentFeature in range(len(allFeatures)):print(allFeatures[ currentFeature ])
    print()
    disMenu()
    print(choice)

main()