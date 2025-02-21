using System;

[Serializable]
public class BodyDescription {
    public BodyDescription(BodyPartDescription all, BodyPartDescription head, BodyPartDescription body,
                           BodyPartDescription arms, BodyPartDescription legs) {

        All = all;
        Head = head;
        Body = body;
        Arms = arms;
        Legs = legs;
    }

    public BodyDescription(BodyPartDescription head, BodyPartDescription body,
                           BodyPartDescription arms, BodyPartDescription legs) {

        Head = head;
        Body = body;
        Arms = arms;
        Legs = legs;
    }

    public BodyPartDescription All { get; private set; }
    public BodyPartDescription Head { get; private set; }
    public BodyPartDescription Body { get; private set; }
    public BodyPartDescription Arms { get; private set; }
    public BodyPartDescription Legs { get; private set; }

    public void SetBodyPartDescription(BodyPartDescription description) {
        switch (description.Type) {
            case BodyPartTypes.All:
                break;

            case BodyPartTypes.Head:
                Head = description;
                break;

            case BodyPartTypes.Body:
                Body = description;
                break;

            case BodyPartTypes.Arms:
                Arms = description;
                break;

            case BodyPartTypes.Legs:
                Legs = description;
                break;

            default:
                break;
        }
    }

    public BodyPartDescription GetBodyPartDescriptionByType(BodyPartTypes partTypes) {
        switch (partTypes) {
            case BodyPartTypes.All:
                return All;

            case BodyPartTypes.Head:
                return Head;

            case BodyPartTypes.Body:
                return Body;

            case BodyPartTypes.Arms:
                return Arms;

            case BodyPartTypes.Legs:
                return Legs;

            default:
                break;
        }

        return null;
    }
}
