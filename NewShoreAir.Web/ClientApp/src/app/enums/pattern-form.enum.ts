export enum PatternFormEnum {
    CUIL_PATTERN = '^(20|23|24|27|30|33)(D)?[0-9]{8}(D)?[0-9]{1}$',
    CUIL_PATTERN_WITH_COMA_SPACE = '^(20|23|24|27|30|33)(D)?[0-9]{8}(D)?[0-9]{1}$',
    NUMBER = '/^-?(0|[1-9]\d*)?$/'
}